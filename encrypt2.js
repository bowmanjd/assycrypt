async function encryptPassword(password, publicKey) {
	// Convert the password to a Uint8Array
	const encoder = new TextEncoder();
	const data = encoder.encode(password);

	// Encrypt the password using RSA-OAEP
	const encrypted = await window.crypto.subtle.encrypt(
		{
			name: "RSA-OAEP",
		},
		publicKey, // Public key in CryptoKey format
		data,
	);

	// Convert the encrypted data to a base64 string for easy transmission
	return btoa(String.fromCharCode(...new Uint8Array(encrypted)));
}

async function main() {
	// Example public key in PEM format (replace with your actual public key)
	const pemPublicKey = `-----BEGIN PUBLIC KEY-----
YOUR_PUBLIC_KEY_HERE
-----END PUBLIC KEY-----`;

	// Import the public key
	const publicKey = await window.crypto.subtle.importKey(
		"spki", // Key format
		pemToArrayBuffer(pemPublicKey), // Convert PEM to ArrayBuffer
		{
			name: "RSA-OAEP",
			hash: "SHA-256",
		},
		true,
		["encrypt"],
	);

	// Encrypt the password
	const password = "mySecurePassword123";
	const encryptedPassword = await encryptPassword(password, publicKey);
	console.log("Encrypted Password:", encryptedPassword);
}

// Helper function to convert PEM to ArrayBuffer
function pemToArrayBuffer(pem) {
	const base64 = pem
		.replace(/-----BEGIN PUBLIC KEY-----/, "")
		.replace(/-----END PUBLIC KEY-----/, "")
		.replace(/\s+/g, "");
	const binaryString = atob(base64);
	const bytes = new Uint8Array(binaryString.length);
	for (let i = 0; i < binaryString.length; i++) {
		bytes[i] = binaryString.charCodeAt(i);
	}
	return bytes.buffer;
}

// Run the main function
main();
