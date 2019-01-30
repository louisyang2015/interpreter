# Compares two text files. 

file1 = "python_output.txt"
file2 = "output.txt"

######################################################


f1 = open(file1, mode="r", encoding="utf-8")
f2 = open(file2, mode="r", encoding="utf-8")

line_number = 1
diff_found = False

for line1 in f1:
    line2 = f2.readline()

    line1 = line1.rstrip()
    line2 = line2.rstrip()

    if line1 != line2:
        # check for a numeric match
        same_number = False

        try:
            line1_num = float(line1.strip())
            line2_num = float(line2.strip())
            if line1_num - line2_num < 1e-6:
                same_number = True
        except:
            pass

        finally:
            if same_number == False:
                print("Line number", line_number, "is different.")
                print(file1 + ":", line1)
                print(file2 + ":", line2)

                diff_found = True
                break
            
    line_number += 1


if diff_found == False:
    print("No difference found.")

f1.close()
f2.close()

