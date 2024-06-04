<!DOCTYPE html>
<html data-theme="light" lang="en">

<head>
    <!-- Meta tags -->
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">

    <!-- Title -->
    <title>{{ $title ?? 'Authentication ' }}</title>

    <!-- Vite asset links -->
    @vite(['resources/css/app.css', 'resources/js/app.js']);
</head>

<body class="font-sans antialiased">
    <!-- Content -->
    <div class="flex items-center justify-center min-h-screen">
        <div class="md:bg-base-200 p-6 shadow-lg w-full max-w-sm">
            {{ $slot }} <!-- Livewire component content -->
        </div>
    </div>

    <!-- Toast notifications -->
    <x-toast />
</body>

</html>
