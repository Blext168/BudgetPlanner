# Wichtige Befehle:

- Zum erstellen einer Andoird APK-Datei:
```bash
dotnet publish -f net9.0-android -c Release -p:AndroidKeyStore=true -p:AndroidSigningKeyAlias=myalias -p:AndroidSigningKeyStore=/voller/pfad/myreleasekey.keystore -p:AndroidSigningStorePass=testSchiefer -p:AndroidSigningKeyPass=testSchiefer -p:PackageFormat=apk
```