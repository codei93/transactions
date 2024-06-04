<div class="md:bg-base-200 p-6 shadow-lg w-full">
    <!-- Header component for "Create Role" -->
    <x-header title="Create Role" />

    <!-- Header component with size, separator, and progress indicator -->
    <x-header size="text-inherit" separator progress-indicator>
        <!-- This header might contain additional content or functionality -->
    </x-header>

    <!-- Card component with form -->
    <x-card class="mt-10 !p-0 sm:!p-2 flex justify-center items-center" shadow>
        <div class="max-w-sm">
            <!-- Form component -->
            <x-form wire:submit="onSubmit">
                <!-- Input field for Role Name -->
                <x-input label="Role Name" value="" wire:model="name" icon="o-shield-exclamation" inline />
                <!-- Actions slot containing Cancel and Save buttons -->
                <x-slot:actions>
                    <!-- Cancel button -->
                    <x-button label="Cancel" type="button" icon="o-arrow-left" link="/roles" class="btn-ghost" />
                    <!-- Save button -->
                    <x-button label="Save" type="submit" icon="o-paper-airplane" class="btn-primary"
                        spinner="onSubmit" />
                </x-slot:actions>
            </x-form>
        </div>
    </x-card>
</div>
