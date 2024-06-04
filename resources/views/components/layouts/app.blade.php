<!DOCTYPE html data-theme="dark">
<html data-theme="light" lang="{{ str_replace('_', '-', app()->getLocale()) }}">

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>{{ $title ?? 'Page Title' }}</title> <!-- Page title -->
    @vite(['resources/css/app.css', 'resources/js/app.js']) <!-- Vite asset links -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js@4.4.0/dist/chart.umd.min.js"></script> <!-- Chart.js library -->
</head>

<body class="font-sans antialiased">
    <!-- Navigation -->
    <x-nav sticky full-width class="border-b-0">
        <x-slot:brand>
            <!-- Branding -->
            <label for="main-drawer" class="lg:hidden mr-3">
                <x-icon name="o-bars-3" class="cursor-pointer" />
            </label>
            <div class="flex items-center gap-2"><x-heroicon-o-arrows-right-left class="w-5 h-5" /> Transactions</div>
        </x-slot:brand>
    </x-nav>
    <!-- Main content -->
    <x-main with-nav full-width>
        <!-- Sidebar -->
        <x-slot:sidebar drawer="main-drawer" collapsible class="bg-base-500">
            @if ($user = auth()->user())
                <!-- User info -->
                <x-list-item :item="$user" value="name" sub-value="email" no-separator no-hover class="pt-2">
                    <x-slot:actions>
                        <!-- Logout button -->
                        <x-button icon="o-power" class="btn-circle btn-ghost btn-xs" tooltip-left="logoff"
                            onclick="logoutModal.showModal()" />
                    </x-slot:actions>
                </x-list-item>
                <x-menu-separator /> <!-- Separator -->
            @endif
            <!-- Logout modal -->
            <x-modal id="logoutModal" title="Are you sure?">
                <div>This action will log you out .</div>
                <x-slot:actions>
                    <!-- Cancel button -->
                    <x-button label="Cancel" class="btn-ghost" onclick="logoutModal.close()" />
                    <!-- Confirm button -->
                    <x-button label="Confirm" wire:click="onLogout()" class="btn-primary" link="/logout" />
                </x-slot:actions>
            </x-modal>
            <!-- Sidebar menu -->
            <x-menu activate-by-route>
                <!-- Dashboard -->
                <x-menu-item title="Dashboard" icon="o-home" link="/dashboard" />
                <!-- Roles & Users (Admin only) -->
                @if (auth()->user()->role['name'] == 'Admin')
                    <x-menu-sub title="Roles & Users" icon="o-shield-exclamation">
                        <x-menu-item title="Roles" icon="o-shield-exclamation" link="/roles" />
                        <x-menu-item title="Users" icon="o-users" link="/users" />
                    </x-menu-sub>
                @endif
                <!-- Transactions -->
                <x-menu-item title="Transactions" icon="o-arrows-right-left" link="/transactions" />
                <!-- Settings -->
                <x-menu-sub title="Settings" icon="o-cog-6-tooth">
                    <!-- Password -->
                    <x-menu-item title="Password" icon="o-key" link="/password" />
                </x-menu-sub>
            </x-menu>
        </x-slot:sidebar>
        <!-- Content -->
        <x-slot:content>
            {{ $slot }} <!-- Livewire component content -->
        </x-slot:content>
    </x-main>
    <!-- Toast notifications -->
    <x-toast />
</body>

</html>
