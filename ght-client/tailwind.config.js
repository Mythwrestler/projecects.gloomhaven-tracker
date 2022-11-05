module.exports = {
  purge: {
    enabled: !process.env.ROLLUP_WATCH,
    content: ["./public/index.html", "./src/**/*.svelte"],
    options: {
      defaultExtractor: (content) => [
        ...(content.match(/[^<>"'`\s]*[^<>"'`\s:]/g) || []),
        ...(content.match(/(?<=class:)[^=>\/\s]*/g) || []),
      ],
    },
  },
  darkMode: "media", // or 'media' or 'class'
  theme: {
    minHeight: {
      '1/2': '50%',
      '2/3': '66%',
      '3/4': '75%',
    },
    extend: {},
  },
  variants: {
    extend: {},
  },
  plugins: [],
};
