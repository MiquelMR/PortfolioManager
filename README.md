# IUserRepository Interface

## Methods

| **Method**                          | **Return Type** | **Description**                                                                 |
|-------------------------------------|-----------------|---------------------------------------------------------------------------------|
| `CreateUserAsync(User user)`        | `Task<bool>`    | Creates a new user and returns `true` if successful.                           |
| `UpdateUserAsync(User user)`        | `Task<bool>`    | Updates an existing user's details and returns `true` if successful.           |
| `DeleteUserByEmailAsync(string email)` | `Task<bool>` | Deletes a user by their email and returns `true` if successful.                |
| `GetUserByEmailAsync(string email)` | `Task<User>`    | Retrieves a user by their email. Returns the user object if found.             |
| `UserExistsByEmailAsync(string email)` | `Task<bool>` | Checks if a user with the given email exists and returns `true` if found.      |

# IAssetRepository Interface

## Methods

| **Method**                          | **Return Type**           | **Description**                                                               |
|-------------------------------------|---------------------------|-------------------------------------------------------------------------------|
| `CreateAssetAsync(Asset asset)`     | `Task<bool>`              | Creates a new asset and returns `true` if successful.                         |
| `UpdateAssetAsync(Asset asset)`     | `Task<bool>`              | Updates an existing asset's details and returns `true` if successful.         |
| `DeleteAssetByNameAsync(string name)` | `Task<bool>`             | Deletes an asset by its name and returns `true` if successful.                |
| `GetAssetByNameAsync(string name)`  | `Task<Asset>`             | Retrieves an asset by its name. Returns the asset object if found.            |
| `GetAssetsAsync()`                  | `Task<ICollection<Asset>>`| Retrieves a collection of all assets.                                         |
| `ExistsByNameAsync(string name)`    | `Task<bool>`              | Checks if an asset with the given name exists and returns `true` if found.    |
