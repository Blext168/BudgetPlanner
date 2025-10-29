export async function isAuthnAvailable() {
    if (!window.PublicKeyCredential)
        return false;

    return await PublicKeyCredential.isUserVerifyingPlatformAuthenticatorAvailable();
}

export async function createCredentialLocal() {
    const challenge = Uint8Array.from(window.crypto.getRandomValues(new Uint8Array(32)));
    const options = {
        challenge,
        rp: { name: "Local Blazor App" },
        user: {
            id: Uint8Array.from("localuser", c => c.charCodeAt(0)),
            name: "localuser",
            displayName: "Local User"
        },
        pubKeyCredParams: [{ alg: -7, type: "public-key" }],
        authenticatorSelection: { authenticatorAttachment: "platform" },
        timeout: 60000,
        attestation: "none"
    };

    const credential = await navigator.credentials.create({ publicKey: options });
    localStorage.setItem("credentialId", btoa(String.fromCharCode(...new Uint8Array(credential.rawId))));
    alert("Biometrisches Login aktiviert!");
}

export async function loginWithLocalCredential() {
    const credentialIdBase64 = localStorage.getItem("credentialId");

    if (!credentialIdBase64)
        throw new Error("Kein Credential gespeichert");

    const challenge = Uint8Array.from(window.crypto.getRandomValues(new Uint8Array(32)));
    const options = {
        challenge,
        allowCredentials: [{
            type: "public-key",
            id: Uint8Array.from(atob(credentialIdBase64), c => c.charCodeAt(0))
        }],
        timeout: 60000,
        userVerification: "preferred"
    };

    const assertion = await navigator.credentials.get({ publicKey: options });
    alert("Biometrischer Login erfolgreich!");
    return true;
}