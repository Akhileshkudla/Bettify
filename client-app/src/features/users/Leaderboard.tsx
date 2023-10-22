import { observer } from 'mobx-react-lite';
import { useEffect } from 'react';
import { Header, Table } from 'semantic-ui-react';
import { useStore } from '../../app/stores/store';

export default observer (function UserForm() {
    const {userStore } = useStore();
    const {users} = userStore;

    useEffect(() =>{
        userStore.getallusers()
    }, [userStore])

      // Sort the users array in decreasing order of 'amount'
  const sortedUsers = users.slice().sort((a, b) => b.amount - a.amount);

  return (
    <Table celled>
      <Table.Header>
        <Table.Row>
            <Table.HeaderCell>Rank</Table.HeaderCell>
          <Table.HeaderCell>Username</Table.HeaderCell>
          <Table.HeaderCell>Amount</Table.HeaderCell>
        </Table.Row>
      </Table.Header>
      <Table.Body>
        {sortedUsers.map((user, index) => (
          <Table.Row key={user.username}>
            <Table.Cell>{index + 1}</Table.Cell>
            <Table.Cell>
                <Header.Content>
                    {user.displayName}
                    <Header.Subheader>{user.username}</Header.Subheader>
                </Header.Content>
            </Table.Cell>
            <Table.Cell >{user.amount}</Table.Cell>            
          </Table.Row>
        ))}
      </Table.Body>
    </Table>
  );
})