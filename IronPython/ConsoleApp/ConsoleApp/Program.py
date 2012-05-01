#Write code that makes the robot move twice and then prints 
#out the resulting distribution, starting with the initial 
#distribution p = [0, 1, 0, 0, 0]

p=[0, 1, 0, 0, 0]
world=['green', 'red', 'red', 'green', 'green']
measurements = ['red', 'green']
pHit = 0.6
pMiss = 0.2
pExact = 0.8
pOvershoot = 0.1
pUndershoot = 0.1

def sense(p, Z):
    q=[]
    for i in range(len(p)):
        hit = (Z == world[i])
        q.append(p[i] * (hit * pHit + (1-hit) * pMiss))
    s = sum(q)
    for i in range(len(q)):
        q[i] = q[i] / s
    return q

def move(p, U):
    q = []
    for i in range(len(p)):
        s = pExact * p[(i-U) % len(p)]
        s = s + pOvershoot * p[(i-U-1) % len(p)]
        s = s + pUndershoot * p[(i-U+1) % len(p)]
        q.append(s)
    return q
#
# ADD CODE HERE
#
for i in range(1000) :
    p=move(p,1)

print p

print 1

#def collectionAssert(e, a):
#    assert len(e) == len(a)
#    for i in range(len(e)) :
#        assert abs(e[i] - a[i]) < 0.000001
#    return
#
##p=[0, 1, 0, 0, 0]
##pExact = 0.8
##pOvershoot = 0.1
##pUndershoot = 0.1
#
#p=[0, 1, 0, 0, 0]
#e=[0,0,0.1,0.8,0.1]
#
#collectionAssert(e, move(p, 2))
#
#p=[0.2, 0.2, 0.2, 0.2, 0.2]
#e=[0.2, 0.2, 0.2, 0.2, 0.2]
#
#collectionAssert(e, move(p, 2))
#
#
#p=[0, 0.5, 0, 0.5, 0]
#e=[0.4, 0.05, 0.05, 0.4, 0.1]
#
#collectionAssert(e, move(p, 2))
#