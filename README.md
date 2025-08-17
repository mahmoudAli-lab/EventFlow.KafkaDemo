# EventFlow.KafkaDemo — Test Suite Scaffold

This repository contains a scaffolded test suite for the EventFlow.KafkaDemo project.

Files added by the scaffold:

- `src/EventFlow.KafkaDemo/` — minimal sample service code used by unit tests.
- `tests/UnitTests/` — unit test project (xUnit).
- `run-tests.ps1` — PowerShell script to run all tests.

Quick start (PowerShell):

```powershell
# restore and run all tests
dotnet restore
./run-tests.ps1
```

Notes
- This is a scaffold for tests (unit + integration + e2e will be added next).
- Integration tests will use Testcontainers to spin up Kafka/Postgres/Mongo in a following change.
