<div class="md:bg-base-200 p-6 shadow-lg w-full">
    <!-- Header component displaying transaction details -->
    <x-header title="Transaction No: {{ $id }} by {{ $data['user']['username'] }}"
        subtitle="Created on {{ date('d-M-Y', strtotime($data['createdAt'])) }}" />
    <!-- Header component with separator and progress indicator -->
    <x-header size="text-inherit" separator progress-indicator>
        <!-- Actions slot containing buttons for editing and deleting transaction -->
        <x-slot:actions>
            <!-- Button to edit transaction -->
            <x-button icon="o-pencil-square" class="btn-primary text-white"
                link="/transactions/update/{{ $id }}" />
            <!-- Button to delete transaction -->
            <x-button icon="o-trash" class="btn-error text-white" onclick="deleteModal.showModal()" />
        </x-slot:actions>
    </x-header>

    <!-- Modal for confirming deletion -->
    <x-modal id="deleteModal" title="Are you sure?">
        <div>This action can not be undone.</div>

        <x-slot:actions>
            <!-- Button to cancel deletion -->
            <x-button label="Cancel" class="btn-ghost" onclick="deleteModal.close()" />
            <!-- Button to confirm deletion -->
            <x-button label="Confirm" wire:click="onDelete({{ $id }})" class="btn-primary"
                spinner="onDelete" />
        </x-slot:actions>
    </x-modal>

    <div class="max-w-lg">
        <!-- Form for displaying and editing transaction details -->
        <x-form wire:submit="onSubmit">
            <!-- Input field for customer names (disabled) -->
            <x-input label="Customer Names" value="{{ $data['customerNames'] }}" icon="o-user" inline disabled />
            <!-- Input field for amount (disabled) -->
            <x-input label="Amount" value="{{ $amount }}" type="number" icon="o-currency-dollar" inline
                disabled />
            <!-- Textarea for description (disabled) -->
            <x-textarea label="Description" value="{{ $description }}" rows="5" inline disabled />
            <!-- Radio buttons for transaction type (disabled) -->
            <x-radio label="Transaction Type" :options="$transactionTypeData" wire:model="transactionType" option-value="value"
                option-label="value" disabled />
            <!-- Radio buttons for payment type (disabled) -->
            <x-radio label="Payment Type" :options="$paymentTypeData" wire:model="paymentType" option-value="value"
                option-label="value" disabled />
        </x-form>
    </div>
</div>
