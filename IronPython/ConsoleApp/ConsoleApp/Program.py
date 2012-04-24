#  Modify your code to create probability vectors, p, of arbitrary 
#  size, n. Use n=5 to verify that your new solution matches 
#  the previous one.

# python create array of size n
# http://stackoverflow.com/questions/1859864/how-to-create-an-integer-array-in-python

n=5
q=[0]*n

for x in range(0, n) :
    q[x] = 1.0/n

print q

p=[]

for y in range(n) :
    p.append(1./n)

print p

