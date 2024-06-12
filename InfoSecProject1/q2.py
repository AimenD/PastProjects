from Crypto.Protocol.KDF import PBKDF2
from Crypto.Hash import SHA512
from Crypto.Random import get_random_bytes
from Crypto.Cipher import AES, PKCS1_OAEP
from cryptography.hazmat.primitives import hashes
from cryptography.hazmat.primitives.serialization import NoEncryption
from cryptography.hazmat.primitives.serialization import Encoding
from cryptography.hazmat.primitives.serialization import PrivateFormat, PublicFormat
from cryptography.hazmat.primitives.asymmetric import ec
from cryptography.hazmat.primitives.kdf.hkdf import HKDF

from q1 import *
from color import *


# Generate symmetric keys using PBKDF2 (Password Based Key Derivation Function 2)
def generateSymmetricKey():
    password = b'this is a password'
    # salt makes algorithm more secure and harder to solve.
    salt = get_random_bytes(16)
    keys = PBKDF2(password, salt, 48, count=1000000,
                  hmac_hash_module=SHA512)  # generate 48 bytes of key
    key1 = keys[:16]  # 128 bit
    key2 = keys[16:]  # 256 bit
    printColored("Symmetric Key 1:", key1.hex())
    printColored("Symmetric Key 2:", key2.hex())
    return [key1, key2]


def q2a(rsa_key):
    printHeader("*** Symmetric Keys *** ")

    [key1, key2] = generateSymmetricKey()

    printHeader("*** Encrypted Symmetric Keys *** ")
    cipher_rsa = PKCS1_OAEP.new(rsa_key)  # Create cipher object
    encrpyted_key1 = cipher_rsa.encrypt(key1) # Encrypt key1 by using cipher object
    encrpyted_key2 = cipher_rsa.encrypt(key2)  # Encrypt key2 by using cipher object
    printColored("Encrypted Key1: ", encrpyted_key1.hex())
    printColored("Encrypted Key2: ", encrpyted_key2.hex())

    printHeader("*** Decrypted Symmetric Keys *** ")
    decrpyted_key1 = cipher_rsa.decrypt(encrpyted_key1)  # Decrypt key1 by using cipher object
    decrpyted_key2 = cipher_rsa.decrypt(encrpyted_key2) # Decrypt key2 by using cipher object
    printColored("Decrypted Key1: ", decrpyted_key1.hex())
    printColored("Decrypted Key2: ", decrpyted_key2.hex())

    return [key1, key2]

# Generate a 256 bit symmetric key using Elliptic key Diffie Helman
def q2b(K_B, K_C):
    printHeader("*** DH Symmetric Keys using KB and KC *** ")

    # Generate a shared key using K_B and K_C (K_B -> private key, K_C -> public key)
    shared_key = K_B.exchange(
        ec.ECDH(), K_C.public_key())

    # Perform key derivation.
    derived_key = HKDF(
        algorithm=hashes.SHA256(),
        length=32,  # 256 bit
        salt=None,
        info=b'handshake data',
    ).derive(shared_key)

    printColored("Derived key from (KB-) and (KC+) :", derived_key.hex())

    # Generate a shared key using K_B and K_C (K_B -> public key, K_C -> private key)
    shared_key_2 = K_C.exchange(
        ec.ECDH(), K_B.public_key())

    # Perform key derivation.
    derived_key_2 = HKDF(
        algorithm=hashes.SHA256(),
        length=32,  # 256 bit
        salt=None,
        info=b'handshake data',
    ).derive(shared_key_2)

    printColored("Derived key from (KB+) and (KC-) :", derived_key_2.hex())

    printColored("Is both keys are same?",
                 derived_key.hex() == derived_key_2.hex())


printHeader("\n---------------2) Generation of Symmetric keys.---------------", color="Red")


symmetric_keys = q2a(K_A["keyPair"])

q2b(K_B, K_C)

