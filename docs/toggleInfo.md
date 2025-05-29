# LoanProduct Toggle Status API - Detailed Explanation

## Overview
The LoanProduct toggle status API allows toggling the `IsActive` status of a loan product (activate/deactivate) via a dedicated endpoint. This is useful for enabling or disabling loan products without deleting them from the database.

---

## 1. Controller Layer
**File:** `LoanProductController.cs`

- **Endpoint:** `PUT /api/LoanProduct/{id}/status`
- **Method:**
  ```csharp
  [HttpPut("{id}/status")]
  public async Task<ActionResult<LoanProductResponseDto>> ToggleStatus(int id)
  {
      var product = await _loanProductService.GetLoanProductByIdAsync(id);
      if (product == null) return NotFound();

      product.IsActive = !product.IsActive; // Toggle status
      var modifiedBy = "system"; // Replace with actual user context
      var updated = await _loanProductService.UpdateLoanProductAsync(product, modifiedBy);
      return Ok(_mapper.Map<LoanProductResponseDto>(updated));
  }
  ```
- **What it does:**
  - Fetches the loan product by ID.
  - Toggles the `IsActive` property (`true` to `false` or vice versa).
  - Calls the service to persist the change.
  - Returns the updated product.

---

## 2. Service Layer
**File:** `LoanProductService.cs`

- **Method:**
  ```csharp
  public async Task<LoanProduct> UpdateLoanProductAsync(LoanProduct loanProduct, string modifiedBy)
  {
      if (loanProduct == null)
          throw new ArgumentNullException(nameof(loanProduct));
      if (string.IsNullOrWhiteSpace(modifiedBy))
          throw new ArgumentException("Modified by cannot be empty", nameof(modifiedBy));

      var existingProduct = await _loanProductRepository.GetByIdAsync(loanProduct.LoanProductId);
      if (existingProduct == null)
          throw new NotFoundException($"Loan product with ID {loanProduct.LoanProductId} not found.");

      // Update fields
      existingProduct.Title = loanProduct.Title;
      existingProduct.Description = loanProduct.Description;
      existingProduct.ImageUrl = loanProduct.ImageUrl;
      existingProduct.MaxLoanAmount = loanProduct.MaxLoanAmount;
      existingProduct.LoanType = loanProduct.LoanType;
      existingProduct.IsActive = loanProduct.IsActive;
      existingProduct.ModifiedAt = DateTime.UtcNow;
      existingProduct.ModifiedBy = modifiedBy;

      // Save changes
      return await _loanProductRepository.UpdateAsync(existingProduct);
  }
  ```
- **What it does:**
  - Validates input.
  - Fetches the existing product from the database.
  - Updates all fields, including `IsActive`.
  - Sets audit fields (`ModifiedAt`, `ModifiedBy`).
  - Calls the repository to persist the update.

---

## 3. Repository Layer
**File:** `LoanProductRepository.cs` (inherits from `GenericRepository`)

- **Method:**
  ```csharp
  public async Task<LoanProduct> UpdateAsync(LoanProduct entity)
  {
      _dbSet.Update(entity);
      await SaveChangesAsync();
      return entity;
  }
  ```
- **What it does:**
  - Marks the entity as updated in the EF context.
  - Saves changes to the database.

---

## 4. Entity Layer
**File:** `LoanProduct.cs`

- **Relevant Property:**
  ```csharp
  public bool IsActive { get; set; }
  ```
- **What it does:**
  - Indicates whether the loan product is active (visible/usable) or inactive (hidden/disabled).

---

## 5. Step-by-Step Flow
1. **Frontend** calls `PUT /api/LoanProduct/{id}/status`.
2. **Controller** fetches the product, toggles `IsActive`, and calls the service.
3. **Service** fetches the latest entity, updates fields, and calls the repository.
4. **Repository** updates the entity in the database.
5. **Database** persists the new `IsActive` value.
6. **Frontend** can now see the updated status in subsequent GET requests (depending on filtering logic).

---

## 6. Notes
- The toggle API does not delete or recreate the product; it only flips the `IsActive` flag.
- Filtering for active/inactive products in GET endpoints is handled separately (e.g., with an `includeInactive` parameter).
- Audit fields (`ModifiedAt`, `ModifiedBy`) are updated for traceability.

---

**This approach provides a clean, auditable, and RESTful way to enable/disable loan products without deleting them.** 

=========================================================
