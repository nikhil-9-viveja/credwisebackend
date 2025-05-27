# CredWiseAdmin Configuration Module: Product Info

## Overview
The configuration module in CredWiseAdmin enables admin users to set up and manage Loan and Fixed Deposit (FD) products, including their types, applications, and transactions. This module is built using a clean architecture with clear separation of concerns across API, Core, Repository, Service, and Utils layers.

---

## Architecture
- **API Layer:** Exposes RESTful endpoints for product configuration and management.
- **Core Layer:** Contains entities, DTOs, and mapping profiles.
- **Repository Layer:** Handles data access using EF Core and repository pattern.
- **Service Layer:** Implements business logic, validation, and error handling.
- **Utils Layer:** (If present) Utility classes and helpers.

---

## Key Entities
- **LoanProduct:** Represents a loan product (Home, Personal, Gold) with details and eligibility.
- **Fdtype:** Represents a type of FD (e.g., 1 year, 3 years) with interest rate, min/max amount, and duration.
- **Fdapplication:** Represents a user's FD application, linked to a user and FD type.
- **Fdtransaction:** Represents a transaction (deposit, payout, withdrawal) on an FD application.

---

## DTOs & Validation
- **DTOs** are used for all API input/output. Strict validation is enforced using data annotations:
  - Required fields, string length, range, and regex for enum-like fields (e.g., TransactionType, PaymentMethod).
  - Example: `TransactionType` must be one of: Deposit, InterestPayout, MaturityPayout, PrematureWithdrawal, Refund.
  - Example: `PaymentMethod` must be one of: NEFT, RTGS, IMPS, UPI, CASH, CHEQUE.
- **Validation errors** return clear messages specifying allowed values.

---

## Error Handling
- **Custom Exceptions:**
  - `BusinessException` for business rule violations (e.g., duplicate FD, invalid status).
  - `NotFoundException` for missing entities (e.g., FD type not found).
- **Global Exception Middleware:**
  - Catches all unhandled exceptions.
  - Returns consistent JSON error responses with appropriate HTTP status codes (400, 404, 500).
  - Logs all exceptions for diagnostics.
- **Controller-Level Validation:**
  - All POST/PUT actions check `ModelState.IsValid` and return 400 with detailed error messages if validation fails.

---

## Mapping
- **AutoMapper** is used to map between entities and DTOs, ensuring all fields are correctly transferred.
- Manual mapping is avoided except for special cases.

---

## Audit Fields
- All entities inherit from `BaseEntity`, which provides:
  - `CreatedAt`, `CreatedBy`, `ModifiedAt`, `ModifiedBy`, `IsActive`.
- These fields are set automatically in the service layer during create/update operations.

---

## FD Module: Key API Patterns
- **FDType:** CRUD for FD types (duration, interest, min/max amount).
- **FDApplication:** CRUD for user FD applications, linked to FDType.
- **FDTransaction:** CRUD for transactions on FD applications. Only valid transaction types and payment methods are accepted.
- **Validation and error handling** are consistent across all endpoints.

---

## Best Practices Implemented
- Only necessary fields are accepted from the UI; system fields are set in the backend.
- All validation and business rules are enforced server-side.
- Error responses are clear, actionable, and consistent.
- All mappings are explicit and tested.
- Audit fields are always populated.

---

## Ready for UI Development
- The backend is robust, with clear contracts (DTOs), validation, and error handling.
- The UI can rely on:
  - Consistent error messages for invalid input.
  - Predictable API responses for all CRUD operations.
  - Well-documented allowed values for all enum-like fields.

---

**For further details, refer to the DTOs and API documentation, or consult the backend team for any business rule clarifications.** 