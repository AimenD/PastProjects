from Crypto.Hash import HMAC, SHA256
from color import *
from q2 import symmetric_keys

# Generate Message Authentication Code
def generateHMAC(keys,m):
    # Question 5.a
    [key1, key2] = keys  # Use symmetric keys (K1, K2) from question 2.
    printHeader("Q5a:")
    secret = key1  #  Use key1 as secret.
    printColored("Key used for HMAC-SHA256: ", key1.hex())
    #  Apply message authentication code.
    h = HMAC.new(secret, digestmod=SHA256)
    h.update(m.encode())  #  Hash and authenticate message m.
    printColored("Message authentication code:", h.hexdigest())

    # Question 5.b
    printHeader("Q5b:")

    h.update(key2)  #  Hash and authenticate key2.
    printColored("Message authentication code for K2:", h.hexdigest())

m="message"
printHeader("\n---------------5) Message Authentication Codes.---------------", color="Red")
generateHMAC(symmetric_keys,m)
