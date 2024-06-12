import numpy as np
import matplotlib.pyplot as plt
from numpy.fft import fft, fftfreq, fftshift
from scipy.io.wavfile import write


# loading the data from text file
myrecording_in_time = np.loadtxt('47.txt')

# Sample rate - set to the correct value for your data!
fs = 96000
seconds = len(myrecording_in_time) / fs  # Calculate the duration based on your sample rate and the number of samples

time = np.linspace(0,seconds,myrecording_in_time.shape[0])

plt.figure(1)
plt.plot(time,myrecording_in_time)

frequencies = np.linspace(-fs/2, fs/2, int(fs*seconds))
myrecording_in_frequency_abs = fftshift(np.abs(fft(myrecording_in_time)))

plt.figure(2)
plt.plot(frequencies , myrecording_in_frequency_abs)

myrecording_in_time_modulated = np.multiply(myrecording_in_time, np.cos(2*np.pi*32000*time))

plt.figure(3)
plt.plot(time,myrecording_in_time_modulated)

myrecording_in_frequency_modulated_abs = fftshift(np.abs(fft(myrecording_in_time_modulated)))

plt.figure(4)
plt.plot(frequencies , myrecording_in_frequency_modulated_abs)

plt.show()


# Rescale the modulated signal to 16-bit integer values
myrecording_in_time_modulated_rescaled = np.int16(myrecording_in_time_modulated / np.max(np.abs(myrecording_in_time_modulated)) * 32767)

# Write the audio data to .wav file
write('modulated_signal.wav', fs, myrecording_in_time_modulated_rescaled)


