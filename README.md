# CodingChallenge

# puzzle

Great puzzle! I enjoyed working on it, but I wasn’t able to meet all the requirements.

Maybe I’m missing something, but there seems to be a conflict between the requirement that the program accept up to 25 letters and the constraint that the answer fit into a 64-bit integer. It seems to me that the highest possible rank of a word is the factorial of the word’s length. Therefore, the highest possible ranking of a word 25 letters in length is the factorial of 25. The maximum word length whose factorial will fit into a 64-bit integer is 20. The factorial of 21 requires a BigInteger (C#).

Factorial of 20 (fits into a long)
2,432,902,008,176,640,000

Factorial of 21 (requires a BigInteger)
51,090,942,171,709,440,000

Factorial of 25 (requires a BigInteger)
15,511,210,043,330,985,984,000,000

Anyway, the program I wrote should work for words up to 20 letters in length. To run the program, execute /bin/Debug/CodingChallenge.exe at the windows command line.

You’ll need to change the attachment file extension from “pdf” to “zip”. Your mail server apparently blocks zip files.

Password for zip file: stack47overflow

Regards,
Greg

