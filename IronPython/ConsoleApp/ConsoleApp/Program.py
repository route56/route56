#Write a code that outputs p after multiplying each entry 
#by pHit or pMiss at the appropriate places. Remember that
#the red cells 1 and 2 are hits and the other green cells
#are misses


p=[0.2,0.2,0.2,0.2,0.2]
pHit = 0.6
pMiss = 0.2

#Enter code here
# http://docs.python.org/tutorial/datastructures.html#more-on-conditions
for x in range(5) :
    if (x == 1 or x == 2) :
        p[x] = p[x]*pHit
    else:
        p[x] = p[x]*pMiss

print p
