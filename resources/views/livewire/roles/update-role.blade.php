<div class="md:bg-base-200 p-6 shadow-lg w-full">
    <!-- Header component for "Update Role" -->
    <x-header title="Update Role" />

    <!-- Header component with size, separator, and progress indicator -->
    <x-header size="text-inherit" separator progress-indicator>
        <!-- Actions slot with delete button -->
        <x-slot:actions>
            <x-button icon="o-trash" class="btn-error text-white" onclick="deleteModal.showModal()" />
        </x-slot:actions>
    </x-header>

    <!-- Card component with modal and form -->
    <x-card class="mt-10 !p-0 sm:!p-2 justify-center items-center" shadow>
        <!-- Modal component for delete confirmation -->
        <x-modal id="deleteModal" title="Are you sure?">
            <div>This action can not be undone.</div>
            <!-- Actions slot with cancel and confirm buttons -->
            <x-slot:actions>
                <x-button label="Cancel" class="btn-ghost" onclick="deleteModal.close()" />
                <x-button label="Confirm" wire:click="onDelete({{ $id }})" class="btn-primary"
                    spinner="onDelete" />
            </x-slot:actions>
        </x-modal>

        <div class="max-w-sm">
            <!-- Form component -->
            <x-form wire:submit="onSubmit">
                <!-- Input field for Role Name -->
                <x-input label="Role Name" value="" wire:model="name" icon="o-shield-exclamation" inline />
                <!-- Actions slot with cancel and save buttons -->
                <x-slot:actions>
                    <x-button label="Cancel" type="button" icon="o-arrow-left" link="/roles" class="btn-ghost" />
                    <x-button label="Save Changes" type="submit" icon="o-paper-airplane" class="btn-primary"
                        spinner="onSubmit" />
                </x-slot:actions>
            </x-form>
        </div>
    </x-card>
</div>
