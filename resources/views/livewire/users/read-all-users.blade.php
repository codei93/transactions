<div class="md:bg-base-200 p-6 shadow-lg w-full">
    <!-- Header component for "Users" -->
    <x-header title="Users" />

    <!-- Header component with separator and progress indicator -->
    <x-header size="text-inherit" separator progress-indicator>
        {{-- SEARCH --}}
        <!-- Slot for search input -->
        <x-slot:title>
            <x-input placeholder="Search By Username" wire:model.live.debounce="search" icon="o-magnifying-glass">
                <!-- Placeholder for search icon -->
                <x-slot:prepend>
                </x-slot:prepend>
            </x-input>
        </x-slot:title>

        {{-- ACTION BUTTON --}}
        <!-- Slot for action button -->
        <x-slot:actions>
            <!-- Button for creating a new user -->
            <x-button label="Create" type="button" icon="o-plus" class="btn-primary" link="/users/create" />
        </x-slot:actions>
    </x-header>

    <!-- Card component containing the user table -->
    <x-card class="mt-10 !p-0 sm:!p-2" shadow>
        <!-- Table component displaying user data -->
        <x-table :headers="$headers" :rows="$data" striped link="/users/update/{id}" />
    </x-card>
</div>
