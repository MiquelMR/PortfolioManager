# IUserRepository Interface

## Methods

| **Method**                          | **Return Type** | **Description**                                                                 |
|-------------------------------------|-----------------|---------------------------------------------------------------------------------|
| `CreateUserAsync(User user)`        | `Task<bool>`    | Creates a new user and returns `true` if successful.                           |
| `UpdateUserAsync(User user)`        | `Task<bool>`    | Updates an existing user's details and returns `true` if successful.           |
| `DeleteUserByEmailAsync(string email)` | `Task<bool>` | Deletes a user by their email and returns `true` if successful.                |
| `GetUserByEmailAsync(string email)` | `Task<User>`    | Retrieves a user by their email. Returns the user object if found.             |
| `UserExistsByEmailAsync(string email)` | `Task<bool>` | Checks if a user with the given email exists and returns `true` if found.      |