<script lang="ts">
    import { Link } from "svelte-navigator";
    import CastleIcon from "../../../common/Icons/CastleIcon.svelte";
    import CompassIcon from "../../../common/Icons/CompassIcon.svelte";
    import GroupIcon from "../../../common/Icons/GroupIcon.svelte";
</script>

<aside
    class="flex flex-col overflow-y-auto overflow-x-hidden bg-white text-gray-700 shadow h-full w-48"
>
    <!-- <aside class="overflow-y-auto overflow-x-hidden flex-grow"></aside> -->
    <!-- Side Nav Bar-->

    <div class="h-16 flex items-center w-full">
        <!-- Logo Section -->
        <Link class="h-6 w-10 mx-auto" to="/campaign">
            <img
                class="h-6 w-10 mx-auto"
                src="https://upload.wikimedia.org/wikipedia/commons/thumb/1/1b/Svelte_Logo.svg/512px-Svelte_Logo.svg.png"
                alt="svelte logo"
            />
        </Link>
    </div>

    <ul class="flex flex-col py-4 space-y-1">
        <li class="hover:bg-gray-100">
            <Link
                to="/campaign"
                class="relative flex flex-row items-center h-11 focus:outline-none hover:bg-gray-50 text-gray-600 hover:text-gray-800 border-l-4 border-transparent hover:border-indigo-500 pr-6"
            >
                <span class="inline-flex justify-center items-center ml-4">
                    <CastleIcon iconClassOverride="h-5 w-5" />
                </span>
                <span class="ml-2 text-sm tracking-wide truncate">
                    Campaign
                </span>
            </Link>
        </li>
        <li class="hover:bg-gray-100">
            <Link
                to="/party"
                class="relative flex flex-row items-center h-11 focus:outline-none hover:bg-gray-50 text-gray-600 hover:text-gray-800 border-l-4 border-transparent hover:border-indigo-500 pr-6"
            >
                <span class="inline-flex justify-center items-center ml-4">
                    <GroupIcon iconClassOverride="h-5 w-5" />
                </span>
                <span class="ml-2 text-sm tracking-wide truncate"> Party </span>
            </Link>
        </li>
        <li class="hover:bg-gray-100">
            <Link
                to="/mission"
                class="relative flex flex-row items-center h-11 focus:outline-none hover:bg-gray-50 text-gray-600 hover:text-gray-800 border-l-4 border-transparent hover:border-indigo-500 pr-6"
            >
                <span class="inline-flex justify-center items-center ml-4">
                    <CompassIcon iconClassOverride="h-5 w-5" />
                </span>
                <span class="ml-2 text-sm tracking-wide truncate">
                    Mission
                </span>
            </Link>
        </li>
    </ul>
</aside>
