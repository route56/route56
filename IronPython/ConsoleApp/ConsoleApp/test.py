# Useage
# import test
# test.collectionAssert(e,a)

# asserts if two collections are same or not.
def collection_assert(expected, actual):
    assert len(expected) == len(actual)
    for i in range(len(expected)) :
        assert abs(expected[i] - actual[i]) < 0.000001
    return

def collection2d_assert(expected, actual):
    assert len(expected) == len(actual)
    for i in range(len(expected)) :
        collection_assert(expected[i],actual[i])
    return
