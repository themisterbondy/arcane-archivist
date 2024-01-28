namespace ArcaneArchivist.SharedKernel;

/// <summary>
///     Represents the result of a validation operation.
/// </summary>
/// <typeparam name="TValue">The type of the validated value.</typeparam>
public class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    /// <summary>
    ///     Represents the result of a validation process.
    /// </summary>
    /// <param name="errors">An array of Error objects representing the validation errors.</param>
    private ValidationResult(Error[] errors) : base(default, false, IValidationResult.ValidataionError)
    {
        Errors = errors;
    }

    /// <summary>
    ///     Gets or sets the array of errors.
    /// </summary>
    /// <remarks>
    ///     This property allows access to the collection of errors that occurred during a process or operation.
    ///     The Errors property is read-only and only allows getting the array of Error objects.
    /// </remarks>
    /// <value>
    ///     An array of Error objects representing the errors.
    /// </value>
    public Error[] Errors { get; }

    /// <summary>
    ///     Creates a new instance of ValidationResult with the specified errors.
    /// </summary>
    /// <param name="errors">An array of Error objects.</param>
    /// <returns>A ValidationResult object.</returns>
    public static ValidationResult<TValue> WithErrors(Error[] errors)
    {
        return new ValidationResult<TValue>(errors);
    }
}