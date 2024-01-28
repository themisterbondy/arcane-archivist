namespace ArcaneArchivist.SharedKernel;

/// <summary>
///     Represents a result of some operation, with status information and possibly an error.
/// </summary>
public class Result
{
    /// <summary>
    ///     Represents the result of an operation.
    /// </summary>
    /// <param name="isSuccess">A boolean value indicating whether the operation was successful.</param>
    /// <param name="error">An instance of the Error class representing the error that occurred during the operation, if any.</param>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the isSuccess parameter is true and the error parameter is not Error.None,
    ///     or when the isSuccess parameter is false and the error parameter is Error.None.
    /// </exception>
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None) throw new InvalidOperationException();

        if (!isSuccess && error == Error.None) throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    ///     Gets a value indicating whether the operation was successful or not.
    /// </summary>
    /// <value>
    ///     <c>true</c> if the operation was successful; otherwise, <c>false</c>.
    /// </value>
    public bool IsSuccess { get; }

    /// <summary>
    ///     Gets a value indicating whether the operation is a failure.
    /// </summary>
    /// <remarks>
    ///     This property returns the negation of the <see cref="IsSuccess" /> property.
    /// </remarks>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    ///     Gets the error property.
    /// </summary>
    /// <value>
    ///     The error.
    /// </value>
    public Error Error { get; }

    /// <summary>
    ///     Creates a new instance of the Result class with a successful result and no error.
    /// </summary>
    /// <returns>A new instance of the Result class with a successful result and no error.</returns>
    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    /// <summary>
    ///     Creates a successful result with the specified value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value to be set.</param>
    /// <returns>A new instance of the result class with the specified value set as the result.</returns>
    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, Error.None);
    }

    /// <summary>
    ///     Creates a failure result with the specified error.
    /// </summary>
    /// <param name="error">The error to associate with the failure result.</param>
    /// <returns>A new <see cref="Result" /> object representing a failure with the specified error.</returns>
    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

    /// <summary>
    ///     Returns a failure result with the specified error.
    /// </summary>
    /// <param name="error">The error associated with the failure.</param>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <returns>A Result object representing a failure with the specified error.</returns>
    public static Result<TValue> Failure<TValue>(Error error)
    {
        return new Result<TValue>(default, false, error);
    }

    /// <summary>
    ///     Creates a new instance of the Result class with the specified value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value to wrap in the Result instance.</param>
    /// <returns>
    ///     A Result instance containing the specified value if it is not null, otherwise a Result instance indicating a
    ///     failure with the Error.NullValue.
    /// </returns>
    public static Result<TValue> Create<TValue>(TValue? value)
    {
        return value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }

    /// <summary>
    ///     Returns the first failure result or success result from the given array of asynchronous result tasks.
    /// </summary>
    /// <param name="results">An array of asynchronous result tasks.</param>
    /// <returns>The first failure result if found, otherwise returns a success result.</returns>
    public static async Task<Result> FirstFailureOrSuccess(params Func<Task<Result>>[] results)
    {
        foreach (var resultTask in results)
        {
            var result = await resultTask();

            if (result.IsFailure) return result;
        }

        return Success();
    }
}
