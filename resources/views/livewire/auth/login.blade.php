<div>
    <!-- Header component -->
    <x-header title="Login" subtitle="Enter your credentials to start your session" with-anchor />

    <!-- Form component -->
    <x-form wire:submit="onSubmit">
        <!-- Username input -->
        <x-input label="Username" value="" wire:model="username" icon="o-user" inline />
        <!-- Password input -->
        <x-input label="Password" value="" wire:model="password" type="password" icon="o-key" inline />

        <!-- Header component for "Don't have an account?" -->
        <x-header subtitle="Don't have an account? " with-anchor>
            <!-- Action button -->
            <x-slot:actions>
                <x-button label="Register Here" type="button" icon="o-arrow-right" class="btn-ghost"
                    link="/register" />
            </x-slot:actions>
        </x-header>

        <!-- Submit button -->
        <x-slot:actions>
            <x-button label="Login" type="submit" icon="o-paper-airplane" class="btn-primary" spinner="onSubmit" />
        </x-slot:actions>
    </x-form>
</div>
