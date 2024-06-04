<div class="md:bg-base-200 p-6 shadow-lg w-full">
    <!-- Header component for "Create Transaction" -->
    <x-header title="Create Transaction" />

    <!-- Header component with separator and progress indicator -->
    <x-header size="text-inherit" separator progress-indicator>
    </x-header>

    <!-- Card component containing the form for creating a transaction -->
    <x-card class="mt-10 !p-0 sm:!p-2 flex justify-center items-center" shadow>
        <div class="max-w-lg">
            <!-- Form component for submitting transaction data -->
            <x-form wire:submit="onSubmit">
                <!-- Input field for customer names -->
                <x-input label="Customer Names" value="" wire:model="customerNames" icon="o-user" inline />
                <!-- Input field for amount -->
                <x-input label="Amount" value="" wire:model="amount" type="number" icon="o-currency-dollar"
                    inline />
                <!-- Textarea for description -->
                <x-textarea label="Description" wire:model="description" rows="5" inline />
                <!-- Radio button for transaction type -->
                <x-radio label="Transaction Type" :options="$transactionTypeData" wire:model="transactionType" option-value="value"
                    option-label="value" />
                <!-- Radio button for payment type -->
                <x-radio label="Payment Type" :options="$paymentTypeData" wire:model="paymentType" option-value="value"
                    option-label="value" />

                <!-- Actions slot for cancel and save buttons -->
                <x-slot:actions>
                    <!-- Button to cancel the transaction -->
                    <x-button label="Cancel" type="button" icon="o-arrow-left" link="/transactions"
                        class="btn-ghost" />
                    <!-- Button to save the transaction -->
                    <x-button label="Save" type="submit" icon="o-paper-airplane" class="btn-primary"
                        spinner="onSubmit" />
                </x-slot:actions>
            </x-form>
        </div>
    </x-card>
</div>
