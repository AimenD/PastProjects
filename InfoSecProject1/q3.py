from cryptography.hazmat.primitives.asymmetric import padding
from cryptography.hazmat.primitives import hashes
from Crypto.Cipher import AES, PKCS1_OAEP
from Crypto.Signature import pkcs1_15
from Crypto.Hash import SHA256

from q1 import *
from color import *



    
# Generate digital signature
def generate_digital_sign(rsa_key):

    image_file = open("image.png", 'rb')  
    image_data = image_file.read()

    hashed_image = SHA256.new(image_data)  # Hashing the image data
    printColored("Hashed using SHA256: ", hashed_image.hexdigest())

    printHeader("-------------------")
    digital_signature = pkcs1_15.new(rsa_key).sign(hashed_image)  # Sign the hashed image data
    printColored("Digital Signature: ", digital_signature.hex())

    printHeader("-------------------")
    # Verify the digital signature and hashed text with public key
    try:
        pkcs1_15.new(rsa_key.publickey()).verify(hashed_image, digital_signature)
        printColored("The signature is valid.")
        printColored("Message Digest H(m): ", hashed_image.hexdigest())
        printColored("Digital Signature: ", digital_signature.hex())
    except (ValueError, TypeError):
        print("The signature is not valid.")


printHeader("\n---------------3) Generation and Verification of Digital Signature.---------------", color="Red")

generate_digital_sign(K_A["keyPair"])
