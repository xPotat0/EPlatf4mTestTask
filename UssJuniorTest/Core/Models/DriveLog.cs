namespace UssJuniorTest.Core.Models;

/// <summary>
/// Запись о вождении.
/// </summary>
/// <remarks>Содержит информацию о временных промежутках вождения конкретного человека конкретным авто.</remarks>
public class DriveLog : Model
{
    /// <summary>
    /// Идентификатор автомобиля.
    /// </summary>
    public long CarId { get; set; } = 3;

    /// <summary>
    /// Идентификатор человека.
    /// </summary>
    public long PersonId { get; set; } = 5;

    /// <summary>
    /// Старт вождения.
    /// </summary>
    public DateTime StartDateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// Конец вождения.
    /// </summary>
    public DateTime EndDateTime { get; set; } = DateTime.Now;
}