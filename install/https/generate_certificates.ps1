openssl genrsa -des3 -out reviewapp-dev-ca.key 2048
openssl req -x509 -new -nodes -key reviewapp-dev-ca.key -subj "/CN=reviewapp-dev-ca" -sha256 -days 1825 -out reviewapp-dev-ca.pem
openssl x509 -outform der -in reviewapp-dev-ca.pem -out reviewapp-dev-ca.crt

openssl genrsa -out dev.reviewapp.com.key 2048
openssl req -new -key dev.reviewapp.com.key -subj "/CN=dev.reviewapp.com" -out dev.reviewapp.com.csr
openssl x509 -req -in dev.reviewapp.com.csr -CA reviewapp-dev-ca.pem -CAkey reviewapp-dev-ca.key -CAcreateserial -out dev.reviewapp.com.crt -days 365 -sha256 -extfile reviewapp.ext
openssl x509 -in dev.reviewapp.com.crt -out dev.reviewapp.com.pem -outform PEM
openssl pkcs12 -export -in dev.reviewapp.com.crt -inkey dev.reviewapp.com.key -out dev.reviewapp.com.pfx