<div class="md:bg-base-200 p-6 shadow-lg w-full">
    <!-- Header component displaying "Update Transaction" -->
    <x-header title="Update Transaction" />
    <!-- Header component with separator and progress indicator -->
    <x-header size="text-inherit" separator progress-indicator>
        <!-- Actions slot containing button for deleting transaction -->
        <x-slot:actions>
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
        <!-- Form for updating transaction details -->
        <x-form wire:submit="onSubmit">
            <!-- Input field for customer names -->
            <x-input label="Customer Names" value="{{ $customerNames }}" wire:model="customerNames" icon="o-user"
                inline />
            <!-- Input field for amount -->
            <x-input label="Amount" value="{{ $amount }}" wire:model="amount" type="number"
                icon="o-currency-dollar" inline />
            <!-- Textarea for description -->
            <x-textarea label="Description" value="{{ $description }}" wire:model="description" rows="5"
                inline />
            <!-- Radio buttons for transaction type -->
            <x-radio label="Transaction Type" :options="$transactionTypeData" wire:model="transactionType" option-value="value"
                option-label="value" />
            <!-- Radio buttons for payment type -->
            <x-radio label="Payment Type" :options="$paymentTypeData" wire:model="paymentType" option-value="value"
                option-label="value" />

            <!-- Actions slot containing buttons for canceling and saving changes -->
            <x-slot:actions>
                <!-- Button to cancel changes -->
                <x-button label="Cancel" type="button" icon="o-arrow-left" link="/transactions" class="btn-ghost" />
                <!-- Button to save changes -->
                <x-button label="Save Changes" type="submit" icon="o-paper-airplane" class="btn-primary"
                    spinner="onSubmit" />
            </x-slot:actions>
        </x-form>
    </div>
</div>
