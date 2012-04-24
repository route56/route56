#Modify the program to find and print the sum of all 
#the entries in the list p.

p=[0.2, 0.2, 0.2, 0.2, 0.2]
pHit = 0.6
pMiss = 0.2

p[0]=p[0]*pMiss
p[1]=p[1]*pHit
p[2]=p[2]*pHit
p[3]=p[3]*pMiss
p[4]=p[4]*pMiss

sum1=0
for i in range(5) :
    sum1 += p[i]

print sum1

#other way
sum2 = sum(p) #if sum1 was named sum, this is error

print sum2
