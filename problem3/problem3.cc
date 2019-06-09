#include <cmath>
#include <iostream>
#include <vector>

using namespace std;

/*findPrimesLessThan finds all of the prime numbers
less than or equal to n and stores them in primes*/
void findPrimesLessThanOrEqualTo(const int n, vector<int> &primes) {
	if (n <= 2) return;
	primes.emplace_back(2);
	int k = 3;
	bool isPrime = true;
	while (k <= n) {
		for (const int prime : primes) {
			if (!(k%prime)) {
				isPrime = false;
				break;
			}
		}
		if (isPrime) {
			primes.emplace_back(k);
		} else {
			isPrime = true;
		}
		k += 2;
	}
}

/*largestPrimeFactor finds the largest prime
factor of the number n*/
int largestPrimeFactor(const long n) {
	vector<int> primes;
	int retval = 0;
	const int UPPER_BOUND = sqrt(n); //prime factors are <= sqrt(n)
	findPrimesLessThanOrEqualTo(UPPER_BOUND, primes);
	for (size_t i = primes.size(); --i; ) {
		if (!(n%primes[i])) {
			retval = primes[i];
			break;
		}
	}
	if (retval == 0) retval = n;
	cout << retval << endl;
	return retval;
}

int main(int argc, char *argv[]) {
	const long N = 600851475143;
	largestPrimeFactor(N);
	return 0;
}
