<?php

use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Route;
use App\Http\Middleware\RedirectIfAuthenticated;

// Importing the necessary classes and middleware

use App\Livewire\Auth\Login;
use App\Livewire\Auth\Register;

use App\Livewire\Dashboard;

use App\Livewire\Roles\CreateRole;
use App\Livewire\Roles\ReadAllRoles;
use App\Livewire\Roles\UpdateRole;

use App\Livewire\Users\CreateUser;
use App\Livewire\Users\ReadAllUsers;
use App\Livewire\Users\UpdateUser;

use App\Livewire\Transactions\CreateTransaction;
use App\Livewire\Transactions\ReadAllTransactions;
use App\Livewire\Transactions\ReadTransaction;
use App\Livewire\Transactions\UpdateTransaction;

use App\Livewire\Settings\UpdatePassword;
use Illuminate\Support\Facades\Session;

// Public routes accessible to unauthenticated users
Route::middleware([RedirectIfAuthenticated::class])->group(function () {
    Route::get('/', Login::class)->name('login');
    Route::get('/register', Register::class)->name('register');
});

// Protected routes accessible only to authenticated users
Route::middleware(['auth'])->group(function () {
    Route::get('/dashboard', Dashboard::class)->name('dashboard');

    // Routes for managing roles
    Route::get('/roles/create', CreateRole::class);
    Route::get('/roles', ReadAllRoles::class);
    Route::get('/roles/update/{id}', UpdateRole::class);

    // Routes for managing users
    Route::get('/users/create', CreateUser::class);
    Route::get('/users', ReadAllUsers::class);
    Route::get('/users/update/{id}', UpdateUser::class);

    // Routes for managing transactions
    Route::get('/transactions/create', CreateTransaction::class);
    Route::get('/transactions', ReadAllTransactions::class);
    Route::get('/transactions/{id}', ReadTransaction::class);
    Route::get('/transactions/update/{id}', UpdateTransaction::class);

    // Routes for updating password and profile settings
    Route::get('/password', UpdatePassword::class);

});

// Route for logging out
Route::get('/logout', function () {
    Session::flush(); // Clear all session data
    Auth::logout(); // Logout the current user

    return Redirect('/'); // Redirect to the login page
});

// End of PHP script
