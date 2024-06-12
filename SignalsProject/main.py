import pandas as pd
import numpy as np

data = pd.read_csv('47.txt', header=None)
data = data[0].values  # convert data to numpy array


from scipy.fftpack import fft
from scipy.fftpack import fftfreq


# Compute FFT
N = len(data)  # Number of data points
T = 1.0 / 800.0  # Sample spacing (this is a hypothetical value and should be replaced by the actual one)
x = np.linspace(0.0, N*T, N)
yf = fft(data)
xf = fftfreq(N, T)[:N//2]

# Plotting the results
import matplotlib.pyplot as plt

plt.plot(xf, 2.0/N * np.abs(yf[0:N//2]))
plt.grid()
plt.show()


from scipy.signal import butter, lfilter

def butter_bandpass(lowcut, highcut, fs, order=5):
    nyq = 0.5 * fs
    low = lowcut / nyq
    high = highcut / nyq
    b, a = butter(order, [low, high], btype='band')
    return b, a

def butter_bandpass_filter(data, lowcut, highcut, fs, order=5):
    b, a = butter_bandpass(lowcut, highcut, fs, order=order)
    y = lfilter(b, a, data)
    return y

# Define the sample rate and the low and high cutoff frequencies
fs = 96000.0  # Sample rate (this is a hypothetical value and should be replaced by the actual one)
lowcut = 500.0  # Low cutoff frequency (this is a hypothetical value and should be replaced by the actual one)
highcut = 8000.0  # High cutoff frequency (this is a hypothetical value and should be replaced by the actual one)

# Apply the filter
y = butter_bandpass_filter(data, lowcut, highcut, fs, order=6)






from scipy.io.wavfile import write
fs = int(fs)

# scale the data to 16-bit integers to match .wav format
scaled = np.int16(y/np.max(np.abs(y)) * 32767)
write('output.wav', fs, scaled)
