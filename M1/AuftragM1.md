# M320 – M1 Klassendiagramm

Szenario: Hotelverwaltung (C#)

## Klassendiagramm

```mermaid
classDiagram
  direction LR

  class Hotel {
    -string name
    -string address
    -int stars
  }

  class Room {
    -int roomNumber
    -int floor
    -decimal pricePerNight
  }

  class Bathroom {
    -bool hasShower
    -bool hasBathtub
    -bool hasToilet
  }

  class Equipment {
    -string name
    -string serialNumber
  }

  class Employee {
    -int employeeNumber
    -string name
    -string role
  }

  Hotel "1" *-- "1..*" Room : owns
  Room "1" *-- "1" Bathroom : has
  Room "0..1" o-- "0..*" Equipment : contains
  Hotel "0..1" o-- "1..*" Employee : employs
```

## Beziehungen

- **Hotel – Room (Komposition, 1 zu 1..*)**
  Zimmer gehören fest zum Hotel. Wird das Hotel gelöscht, werden auch die Zimmer gelöscht.

- **Room – Bathroom (Komposition, 1 zu 1)**
  Jedes Zimmer hat genau ein Badezimmer. Ohne das Zimmer gibt es das Badezimmer nicht.

- **Room – Equipment (Aggregation, 0..1 zu 0..*)**
  Ausstattung kann in ein anderes Zimmer verschoben werden. Sie existiert auch ohne Zimmer.

- **Hotel – Employee (Aggregation, 0..1 zu 1..*)**
  Mitarbeitende bleiben gespeichert, auch wenn das Hotel gelöscht wird.

Faustregel: Kann das Teil ohne das Ganze existieren? Nein = Komposition, Ja = Aggregation.

## Fragen

**Gehören Getter/Setter (Properties) ins Diagramm?**
Nein, nur wichtige Methoden.

**Gehört die Klasse mit der Main-Methode ins Diagramm?**
Nein, sie ist nur der Startpunkt des Programms.

**Wann macht eine bidirektionale Beziehung Sinn?**
Wenn beide Klassen einander kennen müssen.

**Reicht eine Assoziation ohne Pfeil?**
Ja. Ohne Pfeil ist die Richtung offen. Mit Pfeil sieht man, welche Klasse die andere kennt.