# M320 – D1 Kompetenznachweis

Zwei kleine C#-Konsolenprogramme:

| Programm | Was es macht | Klassen |
|---|---|---|
| [Bank](Bank/) | Konten mit Einzahlen, Abheben und Überweisen | `Account` |
| [Heating](Heating/) | Eine Heizung pro Raum, gesteuert von einer Zentrale | `Radiator`, `Controller` |

Starten: im Ordner `Bank` oder `Heating` den Befehl `dotnet run` ausführen.

## 1. Datenkapselung – wie wird der Zugriff auf Attribute gelöst?

Die Attribute sind `private`. Von aussen kann man sie nicht direkt ändern, nur über Methoden.

```csharp
private decimal _balance;                // nur innerhalb von Account sichtbar
public decimal Balance => _balance;      // von aussen nur lesen

public bool Withdraw(decimal amount)
{
    if (amount <= 0 || amount > _balance) return false;   // Prüfung
    _balance -= amount;
    return true;
}
```

Vorteil: Das Objekt prüft selbst, ob eine Änderung erlaubt ist. Das Konto kann nie überzogen werden.
Bei `Radiator.IsOn` ist es ähnlich: `public bool IsOn { get; private set; }` kann jeder lesen, aber nur der Radiator selbst ändern.

## 2. Wie kommunizieren Objekte? Werden Werte mitgegeben?

Objekte werden mit `new` aus einer Klasse erstellt, z.B. `new Account("CH01", "Anna", 1000m)` oder `new Radiator("Bedroom")`.

Objekte rufen Methoden von anderen Objekten auf. Dabei werden Werte als Parameter mitgegeben.

- Bank: `Account.Transfer(Account target, decimal amount)` ruft `target.Deposit(amount, $"transfer from {Number}")` auf. Mitgegeben werden der Betrag und ein Text für die History.
- Heating: `Controller.SetTemperature(room, temperature)` ruft `radiator.SetTarget(temperature)` auf. Mitgegeben wird die Temperatur.

## 3. Wie ändert sich der Zustand eines Objekts?

Der Zustand sind die Werte der Attribute. Er ändert sich nur durch Methodenaufrufe.

- `Deposit` und `Withdraw` ändern `_balance` und fügen einen Eintrag in die History ein.
- `Radiator.Tick()` ändert `_current` (die aktuelle Temperatur) und damit auch `IsOn`.

Beispiel: Ein Raum hat 18 °C, das Ziel ist 20 °C. Der Radiator ist an (`IsOn = true`) und wird mit jedem `Tick()` um 0.5 °C wärmer. Ab 20 °C schaltet er sich aus.

## 4. Primitiver und komplexer Datentyp

| | Primitiv | Komplex |
|---|---|---|
| Was ist es? | Enthält direkt einen Wert | Verweist auf ein Objekt (Referenz) |
| Beispiele im Code | `double _current`, `bool IsOn`, `decimal _balance` (Werttyp) | `Account`, `Radiator`, `List<Account>`, `string` |

Der Unterschied ist wichtig bei der Übergabe:

- Bei einem primitiven Wert wird eine **Kopie** übergeben. Wenn `Deposit(decimal amount)` den Betrag ändern würde, würde das Original nicht betroffen.
- Bei einem komplexen Typ wird die **Referenz** übergeben. Darum verändert `Transfer(Account target, ...)` das echte Zielkonto und nicht eine Kopie.
