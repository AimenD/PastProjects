colors = {
    "Red": "\u001b[31m",
    "Green": "\u001b[32m",
    "Blue": "\u001b[36m",
}

def printColored(string, *args):
    print("\033[1;36m" + string+'\033[0m', *args)

def printHeader(string, color="Green"):
    print("\n" + colors[color]+string+'\033[0m'+"\n")
