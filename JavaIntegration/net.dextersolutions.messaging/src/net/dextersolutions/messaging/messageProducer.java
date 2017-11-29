package net.dextersolutions.messaging;

import java.io.IOException;
import java.util.concurrent.TimeoutException;

import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;

public class messageProducer {
   
	private Connection connection;
   
   
	public messageProducer(String host, int port, String username, String password) throws IOException, TimeoutException {
		ConnectionFactory factory = new ConnectionFactory();
		factory.setHost(host);
		factory.setPort(port);
		factory.setUsername(username);
		factory.setPassword(password);
		 
		this.connection = factory.newConnection();
	}
   
	public void PublishMessage(String exchange, String queue, String routeKey, String message) throws IOException, TimeoutException {
		Channel channel = connection.createChannel();
     
		channel.exchangeDeclare(exchange, "direct", true, false, null);
		channel.queueDeclare(queue, true, false, false, null);
 
		channel.queueBind(queue, exchange, routeKey);
 
		channel.basicPublish("Dexter Printing", "Test", null, message.getBytes("UTF-8"));
  
		channel.close();
		connection.close();
	}
}