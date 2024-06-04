<div class="md:bg-base-200 p-6 shadow-lg w-full">
    <!-- Header component for "Roles" -->
    <x-header title="Roles" />

    <!-- Header component with size, separator, and progress indicator -->
    <x-header size="text-inherit" separator progress-indicator>
        <!-- Title slot with search input -->
        <x-slot:title>
            <x-input placeholder="Search By Role Name" wire:model.live.debounce="search" icon="o-magnifying-glass">
                <x-slot:prepend>
                    <!-- Prepend slot if needed -->
                </x-slot:prepend>
            </x-input>
        </x-slot:title>
        <!-- Actions slot with create button -->
        <x-slot:actions>
            <x-button label="Create" type="button" icon="o-plus" class="btn-primary" link="/roles/create" />
        </x-slot:actions>
    </x-header>

    <!-- Card component with table -->
    <x-card class="mt-10 !p-0 sm:!p-2" shadow>
        <!-- Table component with headers and rows -->
        <x-table :headers="$headers" :rows="$data" striped link="/roles/update/{id}" />
    </x-card>
</div>
