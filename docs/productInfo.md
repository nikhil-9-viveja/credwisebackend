# CredWise Banking System - Product Information

## Overview
CredWise is a comprehensive banking system that offers various financial products including Fixed Deposits (FD) and different types of loans. The system is designed to provide a secure and efficient way for customers to manage their investments and loans.

## Fixed Deposit Products

### FD Types
Fixed Deposits are available in two durations:
- 12 months (1 year)
- 36 months (3 years)

#### FD Type Features
- Minimum deposit amount: ₹1,000
- Maximum deposit amount: ₹10,000,000
- Interest rates vary by duration and type
- Automatic maturity calculation
- Support for premature withdrawal
- Interest payout options

### FD Application Process
1. Customer selects FD type
2. Specifies deposit amount
3. System validates eligibility
4. Application status tracking:
   - Pending
   - Approved
   - Rejected
   - Active
   - Matured
   - PrematureClosed

### FD Transactions
Supported transaction types:
- Deposit
- Interest Payout
- Maturity Payout
- Premature Withdrawal
- Refund

Payment methods:
- NEFT
- RTGS
- IMPS
- UPI
- CASH
- CHEQUE

## Loan Products

### Common Loan Product Features
All loan products share these base attributes:
- Title and Description
- Product Image
- Maximum Loan Amount
- Loan Type Classification
- Active/Inactive Status
- Audit Fields (Created/Modified by/at)

### 1. Home Loans
#### Features
- Maximum loan amount: Up to ₹10,000,000
- Tenure: 1-600 months
- Interest rate: Configurable
- Processing fee: Configurable
- Down payment percentage: Configurable
- Repayment type: EMI

#### Required Documents
- PAN Card
- Aadhaar Card
- Income Proof
- Property Papers

### 2. Personal Loans
#### Features
- Maximum loan amount: Up to ₹10,000,000
- Tenure: 1-600 months
- Interest rate: Configurable
- Processing fee: Configurable
- Minimum salary requirement: Configurable
- Repayment type: EMI

#### Required Documents
- PAN Card
- Aadhaar Card
- Salary Slips (last 3 months)

### 3. Gold Loans
#### Features
- Maximum loan amount: Up to ₹10,000,000
- Tenure: 1-600 months
- Interest rate: Configurable
- Processing fee: Configurable
- Gold purity requirements: Configurable
- Flexible repayment options: Configurable

#### Required Documents
- PAN Card
- Aadhaar Card
- Gold Valuation Report

## Common Features Across Products

### Security
- Secure authentication
- Role-based access control
- Audit logging
- Transaction tracking
- Created/Modified tracking for all entities

### User Management
- User registration and authentication
- Profile management
- Document verification
- Application tracking
- User activity logging

### Transaction Management
- Real-time transaction processing
- Multiple payment method support
- Transaction status tracking
- Automated notifications
- Audit trail for all transactions

### System Architecture
- .NET Core based API
- SQL Server database
- RESTful API architecture
- AutoMapper for object mapping
- Entity Framework Core for data access
- Repository pattern implementation
- Service layer for business logic
- DTO pattern for data transfer

## API Endpoints

### Fixed Deposit Endpoints
- `POST /api/FDApplication` - Create new FD application
- `PUT /api/FDApplication` - Update FD application
- `DELETE /api/FDApplication/{id}` - Delete FD application
- `GET /api/FDApplication/{id}` - Get FD application by ID
- `GET /api/FDApplication` - Get all FD applications

### Loan Product Endpoints
- `GET /api/LoanProduct` - Get all loan products (with optional includeInactive parameter)
- `GET /api/LoanProduct/active` - Get active loan products
- `GET /api/LoanProduct/type/{loanType}` - Get loan products by type
- `GET /api/LoanProduct/{id}` - Get loan product details
- `POST /api/LoanProduct` - Create new loan product
- `POST /api/LoanProduct/home` - Create home loan product
- `POST /api/LoanProduct/personal` - Create personal loan product
- `POST /api/LoanProduct/gold` - Create gold loan product

## Data Validation
All products implement comprehensive validation rules:
- Amount ranges and limits
- Interest rate validation
- Document requirements
- Status transitions
- User eligibility criteria
- Type-specific validation rules
- Business rule validation
- Unique title validation

## Error Handling
The system implements robust error handling:
- Input validation
- Business rule validation
- Transaction rollback
- Error logging
- User-friendly error messages
- Exception handling middleware
- Business exception handling
- Validation exception handling

## Database Schema
The system uses a relational database with the following key entities:
- LoanProduct (Base entity for all loan types)
- HomeLoanDetail
- PersonalLoanDetail
- GoldLoanDetail
- LoanProductDocument
- FDType
- User
- LoanApplication

Each entity includes audit fields (CreatedBy, CreatedAt, ModifiedBy, ModifiedAt) and active status tracking.
