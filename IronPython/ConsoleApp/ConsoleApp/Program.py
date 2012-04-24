#Modify the code below so that the function sense, which 
#takes p and Z as inputs, will output the NON-normalized 
#probability distribution, q, after multiplying the entries 
#in p by pHit or pMiss according to the color in the 
#corresponding cell in world.


p=[0.2, 0.2, 0.2, 0.2, 0.2]
world=['green', 'red', 'red', 'green', 'green']
Z = 'red'
pHit = 0.6
pMiss = 0.2

def sense(p, Z):
    #
    #ADD YOUR CODE HERE
	#
    q = p; # this apporach will modify p. Bad approach. See "other" how they append and create dynamic list
    
    for i in range(len(q)) :
        if (world[i] == Z) :
            q[i] = q[i]*pHit
        else :
            q[i] = q[i]*pMiss
    return q

#other
def sense2(p, Z):
    q = []
    for i in range(len(p)) :
        hit = (Z == world[i])
        q.append(p[i]*(hit * pHit + (1 - hit)*pMiss))
    return q

#print sense(p,Z)
print sense2(p,Z)
print p # p remains unchanged in sense2

#[0.040000000000000008, 0.12, 0.12, 0.040000000000000008, 0.040000000000000008]
#[0.0080000000000000019, 0.071999999999999995, 0.071999999999999995, 0.0080000000000000019, 0.0080000000000000019]

# http://stackoverflow.com/questions/70797/python-and-user-input
