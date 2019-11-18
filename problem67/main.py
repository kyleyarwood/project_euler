def read_file_into_lists():
    lists = None

    with open("triangle.txt") as f:
        lists = f.readlines()

    lists = [l.split(" ") for l in lists]
    lists = [[int(e) for e in l] for l in lists]

    return lists

def find_maximum_path_sum(lists_of_numbers):
    while len(lists_of_numbers) > 1:
        for i in range(len(lists_of_numbers[-2])):
            lists_of_numbers[-2][i] += max(
                lists_of_numbers[-1][i],
                lists_of_numbers[-1][i+1])
        lists_of_numbers.pop()

    return lists_of_numbers[0][0]

def main():
    lists_of_numbers = read_file_into_lists()
    result = find_maximum_path_sum(lists_of_numbers)
    print(result)

if __name__ == "__main__":
    main()
