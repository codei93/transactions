<div class="md:bg-base-200 p-6 shadow-lg w-full">
    <!-- Header component for "Update Password" -->
    <x-header title="Update Password" />

    <!-- Header component with size, separator, and progress indicator -->
    <x-header size="text-inherit" separator progress-indicator>
        <!-- This header might contain additional content or functionality -->
    </x-header>

    <!-- Card component with form -->
    <x-card class="mt-10 !p-0 sm:!p-2 flex justify-center items-center" shadow>
        <div class="max-w-sm">
            <!-- Form component -->
            <x-form wire:submit="onSubmit">
                <!-- Input field for Current Password -->
                <x-input label="Current Password" value="" wire:model="currentPassword" icon="o-key" inline
                    type="password" />

                <!-- Input field for New Password -->
                <x-input label="New Password" value="" wire:model="password" icon="o-key" inline
                    type="password" />

                <!-- Actions slot with cancel and save buttons -->
                <x-slot:actions>
                    <x-button label="Cancel" type="button" icon="o-arrow-left" link="/dashboard" class="btn-ghost" />
                    <x-button label="Save Changes" type="submit" icon="o-paper-airplane" class="btn-primary"
                        spinner="onSubmit" />
                </x-slot:actions>
            </x-form>
        </div>
    </x-card>
</div>
