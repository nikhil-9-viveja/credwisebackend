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

### 1. Home Loans
#### Features
- Maximum loan amount: Up to ₹10,000,000
- Tenure: 1-600 months
- Interest rates: 0.1% - 100%
- Processing fee: Up to ₹1,000,000
- Down payment percentage: 0-100%

#### Required Documents
- PAN Card
- Aadhaar Card
- Income Proof
- Property Papers

### 2. Personal Loans
#### Features
- Maximum loan amount: Up to ₹10,000,000
- Tenure: 1-600 months
- Interest rates: 0.1% - 100%
- Processing fee: Up to ₹1,000,000
- Minimum salary requirement: Up to ₹10,000,000

#### Required Documents
- PAN Card
- Aadhaar Card
- Salary Slips (last 3 months)

### 3. Gold Loans
#### Features
- Maximum loan amount: Up to ₹10,000,000
- Tenure: 1-600 months
- Interest rates: 0.1% - 100%
- Processing fee: Up to ₹1,000,000
- Gold purity requirements
- Flexible repayment options

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

### User Management
- User registration and authentication
- Profile management
- Document verification
- Application tracking

### Transaction Management
- Real-time transaction processing
- Multiple payment method support
- Transaction status tracking
- Automated notifications

### System Requirements
- .NET Core based API
- SQL Server database
- RESTful API architecture
- AutoMapper for object mapping
- Entity Framework Core for data access

## API Endpoints

### Fixed Deposit Endpoints
- `POST /api/FDApplication` - Create new FD application
- `PUT /api/FDApplication` - Update FD application
- `DELETE /api/FDApplication/{id}` - Delete FD application
- `GET /api/FDApplication/{id}` - Get FD application by ID
- `GET /api/FDApplication` - Get all FD applications

### Loan Product Endpoints
- `POST /api/LoanProduct` - Create new loan product
- `PUT /api/LoanProduct` - Update loan product
- `GET /api/LoanProduct/{id}` - Get loan product details
- `GET /api/LoanProduct` - Get all loan products

## Data Validation
All products implement comprehensive validation rules:
- Amount ranges
- Interest rate limits
- Document requirements
- Status transitions
- User eligibility criteria

## Error Handling
The system implements robust error handling:
- Input validation
- Business rule validation
- Transaction rollback
- Error logging
- User-friendly error messages
