# MotorcycleWorkshop-C-

Aufgabenstellung:

Hinweise zur praktischen Arbeit in POS - Einzelarbeit pro Studierende(r)
       (muss beim Lehrer vorgeführt werden)

Implementieren Sie eine Applikation nach dem Code First Prinzip. Diese Applikation muss eine eigenständige Implementierung und somit eine Implementierung unabhängig von den Beispielen bzw. Übungsaufgaben vom Unterricht umfassen.

Mindestanforderungen:

    Mindestens 5 Modelklassen, wobei zwischen mindestens 2 Modelklassen eine Vererbung mit einem Discriminator implementiert sein muss.
    Umsetzung von Value Objects und von Properties für die Fremdschlüsselfelder.
    Eine Modelkassen ist ein Aggregate mit einer Readonly Collection und einem Backing Field.
    Auf dem Aggregate sind eine Methode zum Hinzufügen von Instanzen einer anderen Modelklasse und eine Methode zum Umwandeln eines Objektes innerhalb der Vererbungskette implementiert (siehe Methoden TryHandIn() und ReviewHandIn() von der Übung RichDomainModels als Beispiele).
    Es sind weitere 4 Methoden bzw. Properties mit einer entsprechenden Funktionalität implementiert (siehe Methoden GetActiveTasks() bzw. CalculateAveragePoints() von der Übung RichDomainModels als Beispiele). Verwenden Sie auch LINQ Statements für die Methoden.
    Das Erstellen der Datenbank entsprechend den Modelkassen ist als Testfall implementiert.
    Für jede Methode ist ein eigener Success Test, der das Testergebnis aus der Datenbank ausliest, in einem XUnit Testprojekt umgesetzt.

    
Erweiterungsmöglichkeiten für eine bessere Beurteilung:

    Umsetzung von beidseitigen Navigations.
    Zusätzliche Modelkassen bzw. Methoden mit entsprechenden Funktionalitäten sind implementiert.
    Die Datenbank wird mit den in den Modelklassen enthaltenen Datentypdefinitionen erstellt und mit passenden Testdaten befüllt.
    GUID Werte werden als Alternate Keys verwendet.
    Umsetzung von Lazy Loading.
    Zusätzliche XUnit Testfälle sind implementiert.
