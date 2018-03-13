# Import of the specialization to mongo db
To import doctor's specializations to mongo db execute the following command

    mongoimport -h database_host --port port_number -d database_name -c collection_name -u user -p ***** --type json --file install/static-data/specializations.json

Please that currently the list of the doctor's specializations is provided only Ukrainian language