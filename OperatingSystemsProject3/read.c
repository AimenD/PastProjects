#include <stdio.h>
#include <ctype.h>
#include <string.h>
#include <pthread.h>
#include <semaphore.h>



#define LINES 5
#define LEN 50
char array[LINES][LEN];

sem_t read_sem;






void* read(void *thread_args)
{
  
  FILE *f;
  
  f = fopen("deneme.txt", "r");
  
  int line = 0;

  while (!feof(f) && !ferror(f))
    if (fgets(array[line], LEN, f) != NULL)
      line++;

  

  fclose(f);

  printf("\nFile lines are: \n ");
  for (int i = 0; i < line; i++)
    printf("%s", array[i]);

  sem_post(&read_sem);
}






void* upper() {

    sem_wait(&read_sem);
    
    int num = 5, i, j;

    for (i = 0; i < num; i++) {
        for (j = 0; array[i][j]; j++) {
            array[i][j] = toupper(array[i][j]);
        }
    }

    printf("\n\n upperThread made String into: \n");
    for (i = 0; i < num; i++) {
        printf("%s\n", array[i]);
    }
    
    sem_post(&read_sem);
}



void* replace() {

    sem_wait(&read_sem);

    

    for (int i = 0; i < LINES; i++) {
        int len = strlen(array[i]);
        for (int j = 0; j < len; j++) {
            if (array[i][j] == ' ') {
                array[i][j] = '_';
            }
        }
    }


    printf("replaceThread Replaced space with underscore : \n");

    for (int i = 0; i < LINES; i++) {
        printf("%s\n", array[i]);
    }


    sem_post(&read_sem);


}




void* write()
{
  FILE *f = fopen("deneme.txt", "wb");
  fwrite(array, sizeof(char), sizeof(array), f);
  fclose(f);
}







int main()
{


    sem_init(&read_sem,0,0);


    pthread_t readThread, upperThread, replaceThread, writeThread;

    pthread_create(&readThread, NULL ,read, NULL  );
  
    pthread_create(&upperThread, NULL ,upper, NULL  );

    pthread_create(&replaceThread, NULL ,replace, NULL  );

    pthread_create(&writeThread, NULL ,write, NULL  );

    pthread_join(readThread, NULL);

    pthread_join(upperThread, NULL);

    pthread_join(replaceThread, NULL);

    pthread_join(writeThread, NULL);

    sem_destroy(&read_sem);

  
  //printf("%s",array[1]);

  
}
