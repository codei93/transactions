<div class="md:bg-base-200 p-6 shadow-lg w-full">
    <!-- Header component for "Transactions" -->
    <x-header title="Transactions" />

    <!-- Header component with separator and progress indicator -->
    <x-header size="text-inherit" separator progress-indicator>
        <!-- Title slot containing search input -->
        <x-slot:title>
            <x-input placeholder="Search By Customer" wire:model.live.debounce="search" icon="o-magnifying-glass">
                <x-slot:prepend>
                </x-slot:prepend>
            </x-input>
        </x-slot:title>

        <!-- Actions slot containing button for creating a new transaction -->
        <x-slot:actions>
            <x-button label="Create" type="button" icon="o-plus" class="btn-primary" link="/transactions/create" />
        </x-slot:actions>
    </x-header>

    <!-- Card component containing table of transactions -->
    <x-card class="mt-10 !p-0 sm:!p-2" shadow>
        <!-- Table component displaying transaction data -->
        <x-table :headers="$headers" :rows="$data" striped link="/transactions/{id}" />
    </x-card>
</div>
