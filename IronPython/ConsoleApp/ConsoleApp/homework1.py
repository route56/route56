def sense_and_move(colors, measurements, sensor_right):
    # initial prob distribution assuming uniform.
    size=0
    for i in range(len(colors)) :
        for j in range(len(colors[i])) :
            size += 1

    q = [[1./size for j in range(len(colors[i]))] for i in range(len(colors))]

    q = sense(colors, q, measurements[0], sensor_right)

    return q

def sense(colors, p, Z, sensor_right):
    pHit = sensor_right;
    pMiss = 1 - pHit;

    q = [[p[i][j] * ((Z == colors[i][j]) * pHit + (1-(Z == colors[i][j])) * pMiss) for j in range(len(p[i]))] for i in range(len(p))]

    s=0
    for i in range(len(q)) :
        s += sum(q[i])

    q = [[q[i][j] / s for j in range(len(q[i]))] for i in range(len(q))]

    return q

def move(p, U):
    q = []
    for i in range(len(p)):
        s = pExact * p[(i-U) % len(p)]
        s = s + pOvershoot * p[(i-U-1) % len(p)]
        s = s + pUndershoot * p[(i-U+1) % len(p)]
        q.append(s)
    return q
##
## ADD CODE HERE
##
#for i in range(len(motions)) :
#    p = sense(p, measurements[i])
#    p = move(p, motions[i])


# Why doesn't below work?
#q=[]
##r=[]
#for i in range(len(colors)) :
#    r=[]
#    for j in range(len(colors[i])) :
#        r.append(1./size)
#    q.append(r)
#    #r=[]
# http://stackoverflow.com/questions/2397141/how-to-initialize-a-two-dimensional-array-in-python
#bar = []
#for item in some_iterable:
#    bar.append(SOME EXPRESSION)
#bar = [SOME EXPRESSION for item in some_iterable]
