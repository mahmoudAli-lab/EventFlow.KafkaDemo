# Run all tests in repository
$ErrorActionPreference = 'Stop'

Write-Host "Running all unit, integration, e2e and performance tests..."

# Run unit tests fast first
dotnet test "tests/UnitTests/UnitTests.csproj" --no-build --logger "console;verbosity=normal"

# Integration tests (may spin containers)
dotnet test "tests/IntegrationTests/IntegrationTests.csproj" --no-build --logger "console;verbosity=normal"

# End-to-end tests
dotnet test "tests/E2ETests/E2ETests.csproj" --no-build --logger "console;verbosity=normal"

# Performance tests
dotnet test "tests/PerformanceTests/PerformanceTests.csproj" --no-build --logger "console;verbosity=normal"

Write-Host "All test runs completed."
