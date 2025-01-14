# Windows Service

Applicazione progettata per essere eseguita come **servizio Windows**. Questo servizio registra eventi periodici e attività di avvio e arresto in un file di log.

## Caratteristiche
- Scrive un messaggio di avvio nel file di log quando il servizio viene avviato.
- Registra un messaggio ogni 5 secondi per monitorare l'esecuzione periodica del servizio.
- Scrive un messaggio di arresto nel file di log quando il servizio viene fermato.
- Gestisce automaticamente la creazione della directory e del file di log se non esistono.

## Requisiti
- **.NET Framework 4.0 o superiore**
- Accesso sufficiente al file system per creare directory e file.

## Installazione
1. Compila il progetto in **Visual Studio 2022** o una versione compatibile.
2. Usa lo strumento `InstallUtil.exe` per installare il servizio. Ad esempio:
   ```cmd
   cd C:\Windows\Microsoft.NET\Framework\v4.0.30319
   InstallUtil.exe "PercorsoCompleto\ProvaService.exe"
3. Avvia il servizio tramite **Gestione servizi** di Windows o col comando:
   ```cmd
   sc start ProvaService

## Configurazione
**Log Directory**
Il servizio crea automaticamente una directory chiamata Logs nella directory base dell'applicazione. I file di log hanno un nome nel formato:
  ```cmd
  ServiceLog_gg_mm_aaaa.txt
  ```
**Modifica dell'intervallo**
L'intervallo di tempo tra le esecuzioni del metodo periodico è configurato a 5 secondi (5000 ms). Puoi modificarlo nel metodo OnStart:

  ```csharp
  timer.Interval = 5000; // Intervallo in millisecondi
  ```
**File di log**
Il servizio scrive i seguenti messaggi nei file di log:

1. **Avvio del servizio:**
  ```css
  Servizio iniziato alle [data e ora]
  ```
2. **Evento periodico:**
```css
Il servizio è stato richiamato alle [data e ora]
```
3. **Arresto del servizio:**
  ```css
  Il servizio si è fermato alle [data e ora]
  ```
## Metodo principale
**Funzionalità principali:**
- OnStart:
  - Avvia il servizio.
  - Configura un timer per eseguire attività periodiche.

- OnElapsedTime:
  - Eseguito ogni 5 secondi. Registra l'ora corrente nel file di log.
- OnStop:
  - Ferma il servizio e registra un messaggio nel file di log.

## Disinstallazione
Per disinstallare il servizio, utilizza nuovamente InstallUtil.exe:

``` cmd
InstallUtil.exe /u "PercorsoCompleto\ProvaService.exe"
```

## Note
- Per eseguire le operazioni di installazione/disinstallazione, assicurati di avviare il terminale come Amministratore.
- Controlla i file di log nella directory Logs per verificare il corretto funzionamento del servizio.

## Esempio di Output nel File di Log
```
Servizio iniziato alle 14/01/2025 10:00:00
Il servizio è stato richiamato alle 14/01/2025 10:00:05
Il servizio è stato richiamato alle 14/01/2025 10:00:10
Il servizio si è fermato alle 14/01/2025 10:00:15
```

## Contatti
Per qualsiasi domanda o supporto, contattami: [vg.giannellivaleria@gmail.com].













