import svelte from 'rollup-plugin-svelte';
import commonjs from '@rollup/plugin-commonjs';
import resolve from '@rollup/plugin-node-resolve';
import livereload from 'rollup-plugin-livereload';
import { terser } from 'rollup-plugin-terser';
import sveltePreprocess from 'svelte-preprocess';
import typescript from '@rollup/plugin-typescript';
import css from 'rollup-plugin-css-only';
import dotenv from 'dotenv';
import replace from '@rollup/plugin-replace';

dotenv.config({ path: `.env.${process.env.NODE_ENV}` })

const production = (process.env.NODE_ENV ?? 'local') === 'production';
const isBuildOnly = !process.env.ROLLUP_WATCH;

function serve() {
	let server;

	function toExit() {
		if (server) server.kill(0);
	}

	return {
		writeBundle() {
			if (server) return;
			server = require('child_process').spawn('npm', ['run', 'start', '--', '--dev'], {
				stdio: ['ignore', 'inherit', 'inherit'],
				shell: true
			});

			process.on('SIGTERM', toExit);
			process.on('exit', toExit);
		}
	};
}

export default {
	input: 'src/main.ts',
	output: {
		sourcemap: true,
		format: 'iife',
		name: 'app',
		file: 'public/build/bundle.js'
	},
	plugins: [
		svelte({
			preprocess: sveltePreprocess({ sourceMap: !production, postcss: true, }),
			compilerOptions: {
				// enable run-time checks when not in production
				dev: !production
			},
		}),
		// we'll extract any component CSS out into
		// a separate file - better for performance
		css({ output: 'bundle.css' }),

		// If you have external dependencies installed from
		// npm, you'll most likely need these plugins. In
		// some cases you'll need additional configuration -
		// consult the documentation for details:
		// https://github.com/rollup/plugins/tree/master/packages/commonjs
		resolve({
			browser: true,
			dedupe: ['svelte']
		}),
		commonjs(),
		typescript({
			sourceMap: !production,
			inlineSources: !production
		}),
		
		replace({
			preventAssignment: true,
			ENV_CLIENT_BASE_URL: process.env.CLIENT_BASE_URL ?? "",
			ENV_AUTH_ENABLED: process.env.AUTH_ENABLED ?? "false",
			ENV_AUTH_DOMAIN: process.env.AUTH_DOMAIN ?? "",
			ENV_AUTH_CLIENT_ID: process.env.AUTH_CLIENT_ID ?? "",
			ENV_AUTH_API_AUDIENCE: process.env.AUTH_API_AUDIENCE ?? "",
			ENV_API_BASE_URL: process.env.API_BASE_URL ?? ""
		}),

		// In dev mode, call `npm run start` once
		// the bundle has been generated
		!isBuildOnly && serve(),

		// Watch the `public` directory and refresh the
		// browser on changes when not in production
		!isBuildOnly && livereload('public'),

		// If we're building for production (npm run build
		// instead of npm run dev), minify
		isBuildOnly && terser()
	],
	watch: {
		clearScreen: false
	}
};
