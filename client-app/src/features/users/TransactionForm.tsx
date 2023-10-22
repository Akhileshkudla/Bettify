import { observer } from 'mobx-react-lite';
import { useEffect } from 'react';
import { Segment, Table, Image, Label } from 'semantic-ui-react';
import { useStore } from '../../app/stores/store';
import { format } from 'date-fns';


export default observer (function TranscationForm() {
    const { transactionStore } = useStore();
    const { transcations } = transactionStore;

    useEffect(() =>{
        transactionStore.getTransaction();
    }, [transactionStore])

  return (
    <>
    <Segment>
      <Table celled>
        <Table.Header>
          <Table.Row>
            <Table.HeaderCell>Date</Table.HeaderCell>
            <Table.HeaderCell>Name</Table.HeaderCell>
            <Table.HeaderCell>Amount</Table.HeaderCell>
            <Table.HeaderCell>Message</Table.HeaderCell>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {transcations.map((transaction) => (
            <Table.Row key={transaction.id}>
              <Table.Cell>{
                format(new Date(transaction.date!), 'dd MMM yyyy h:mm aa')
              }</Table.Cell>
              <Table.Cell>{transaction.name}</Table.Cell>
              <Table.Cell>{transaction.amount}</Table.Cell>
              <Table.Cell>{transaction.message}</Table.Cell>
            </Table.Row>
          ))}
        </Table.Body>
      </Table>
    </Segment>
      <Label content="Find the older (WhatsApp) calculations as below:" />
      <Image  alt='Data' src={'https://res.cloudinary.com/dtpzeuru1/image/upload/v1698012653/hpqae9fjzy6jau77ay5d.png'} />
    </>
  );
})