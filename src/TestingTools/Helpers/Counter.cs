namespace TestingTools.Helpers
{
  using System;

  /// <summary>
  /// Contador que tiene la capacidad de 'brincarse cuentas' con una frecuencia específica
  /// </summary>
  /// <typeparam name="T">Tipo int, double, float, short o byte, sino truena :)</typeparam>
  public class Counter<T>
  {
    private object value = (double)0;
    private int addCount = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="Counter&lt;T&gt;"/> class.
    /// </summary>
    /// <param name="skipFrequency">The skip frequency.</param>
    public Counter(int skipFrequency = 0)
    {
      if (!typeof(T).IsValueType)
        throw new ArgumentException("Type: <T> must be a value type.");
      if (!typeof(T).IsPrimitive)
        throw new ArgumentException("Type: <T> must be a primitive (built-in) type.");

      this.Skip = skipFrequency + 1;
    }

    /// <summary>
    /// Gets or sets the value of the Counter
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public T Value
    {
      get
      {
        return (T)Convert.ChangeType(this.value, typeof(T));
      }

      set
      {
        this.value = Convert.ChangeType(this.value, typeof(T));
      }
    }

    /// <summary>
    /// Gets or sets the frequency of ++ operations to ignore
    /// </summary>
    /// <value>
    /// The skip.
    /// </value>
    public int Skip
    {
      get;
      set;
    }

    public static Counter<T> operator +(Counter<T> counter, Counter<T> counter2)
    {
      double numberValue = (double)Convert.ChangeType(counter.value, typeof(double));
      double secondNumber = (double)Convert.ChangeType(counter2.value, typeof(double));
      double result = numberValue + secondNumber;

      return new Counter<T>
      {
        value = result
      };
    }

    public static Counter<T> operator +(Counter<T> counter, T number)
    {
      double numberValue = (double)Convert.ChangeType(counter.value, typeof(double));
      double secondNumber = (double)Convert.ChangeType(number, typeof(double));

      double result = numberValue + secondNumber;


      return new Counter<T>
      {
        value = result,
        Skip = counter.Skip,
        addCount = counter.addCount
      };
    }

    /// <summary>
    /// Implements the operator ++.
    /// </summary>
    /// <param name="counter">The counter.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static Counter<T> operator ++(Counter<T> counter)
    {
      counter.addCount++;
      if (counter.addCount % (counter.Skip + 1) == 0)
      {
        double numberValue = (double)Convert.ChangeType(counter.value, typeof(double));
        numberValue++;
        counter.value = numberValue;
      }

      return new Counter<T>
      {
        value = counter.value,
        Skip = counter.Skip,
        addCount = counter.addCount
      };
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Counter&lt;T&gt;"/> to <see cref="T"/>.
    /// </summary>
    /// <param name="counter">The counter.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator T(Counter<T> counter)
    {
      return counter.Value;
    }
  }
}
