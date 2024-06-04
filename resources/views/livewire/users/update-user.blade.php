<!-- Container div with background, padding, shadow, and full width -->
<div class="md:bg-base-200 p-6 shadow-lg w-full">

    <!-- Header component with title "Update Role" -->
    <x-header title="Update User" />

    <!-- Header component with inherited text size, separator, and progress indicator -->
    <x-header size="text-inherit" separator progress-indicator>

        {{-- ACTION BUTTONS --}}
        <!-- Slot for action buttons in the header -->
        <x-slot:actions>
            <!-- Button with trash icon, error style, and onclick event to show delete modal -->
            <x-button icon="o-trash" class="btn-error text-white" onclick="deleteModal.showModal()" />
        </x-slot:actions>
    </x-header>

    <!-- Card component with margin, padding, center alignment, and shadow -->
    <x-card class="mt-10 !p-0 sm:!p-2 flex justify-center items-center" shadow>

        {{-- DELETE MODAL --}}
        <!-- Modal component for delete confirmation with ID "deleteModal" and title -->
        <x-modal id="deleteModal" title="Are you sure?">
            <div>This action can not be undone.</div>

            <!-- Slot for action buttons in the modal -->
            <x-slot:actions>
                <!-- Button to cancel and close the modal -->
                <x-button label="Cancel" class="btn-ghost" onclick="deleteModal.close()" />
                <!-- Button to confirm deletion with click event handler and spinner -->
                <x-button label="Confirm" wire:click="onDelete({{ $id }})" class="btn-primary"
                    spinner="onDelete" />
            </x-slot:actions>
        </x-modal>

        <!-- Container div with maximum width -->
        <div class="max-w-sm">
            <!-- Form component with submit event handler -->
            <x-form wire:submit="onSubmit">
                <!-- Input field for username with user icon and two-way data binding -->
                <x-input label="Username" value="" wire:model="username" icon="o-user" inline />
                <!-- Input field for email with at-symbol icon and two-way data binding -->
                <x-input label="Email" value="" wire:model="email" icon="o-at-symbol" inline />
                <!-- Radio button group for selecting role with options, value, and label -->
                <x-radio label="Select Role" :options="$roles" option-value="id" option-label="name"
                    wire:model="roleId" />

                <!-- Slot for form action buttons -->
                <x-slot:actions>
                    <!-- Button to cancel and link back to users page -->
                    <x-button label="Cancel" type="button" icon="o-arrow-left" link="/users" class="btn-ghost" />
                    <!-- Button to save changes with submit type and spinner -->
                    <x-button label="Save Changes" type="submit" icon="o-paper-airplane" class="btn-primary"
                        spinner="onSubmit" />
                </x-slot:actions>
            </x-form>
        </div>
    </x-card>
</div>
