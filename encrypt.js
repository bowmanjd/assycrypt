async function encrypt(publicKey, plaintext) {
	const encoder = new TextEncoder();
	const data = encoder.encode(plaintext);
	const encryptedData = await window.crypto.subtle.encrypt(
		{
			name: "RSA-OAEP",
		},
		publicKey,
		data,
	);
	const cipherbytes = new Uint8Array(encryptedData);
	const ciphertext = btoa(String.fromCharCode(...cipherbytes));
	return ciphertext;
}
